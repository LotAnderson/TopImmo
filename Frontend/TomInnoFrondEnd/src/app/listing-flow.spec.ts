import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { ActivatedRoute, provideRouter } from '@angular/router';
import { RealEstateComponent } from './component/realestate.component/realestate.component';
import { StutgartsmapComponent } from './pages/stutgartsmap.component/stutgartsmap.component';
import { ListingDetailComponent } from './pages/listing-detail.component/listing-detail.component';
import { DistrictFilterService } from './services/district-filter';
import { environment } from '../environments/environment';

const url = `${environment.apiUrl}/realestate`;
const listing = { id: '1', title: 'Apartment in Mitte', price: 900, livingSpace: 50, rooms: 2,
  pictureUrls: ['photo.jpg'], address: { line: 'Stuttgart, Mitte' }, attributes: [], mappedDistrict: 'Mitte' };
const response = { listings: [listing], districtCounts: [{ district: 'Mitte', count: 7 }],
  mapDistricts: { a101_Oberer_Schlossgarten_3_: 'Mitte', a121_Relenberg_3_: 'Nord',
    a521_Obertürkheim_3_: 'Obertürkheim' } };

describe('Listing API and display flow', () => {
  let http: HttpTestingController;
  beforeEach(() => {
    TestBed.configureTestingModule({ providers: [provideHttpClient(), provideHttpClientTesting(), provideRouter([]),
      { provide: ActivatedRoute, useValue: { snapshot: { paramMap: new Map([['id', '1']]) } } }] });
    http = TestBed.inject(HttpTestingController);
  });
  afterEach(() => http.verify());

  it('shares the overview request and displays backend counts unchanged', () => {
    const cards = TestBed.createComponent(RealEstateComponent);
    const map = TestBed.createComponent(StutgartsmapComponent);
    cards.detectChanges(); map.detectChanges();
    http.expectOne(`${url}/stuttgart-listings`).flush(response);
    cards.detectChanges(); map.detectChanges();
    expect(cards.nativeElement.textContent).toContain('Apartment in Mitte');
    expect(map.componentInstance.districtCounts()).toEqual(response.districtCounts);
    cards.destroy(); map.destroy();
  });

  it('uses backend map mapping to request a district and displays empty results', () => {
    const cards = TestBed.createComponent(RealEstateComponent);
    const map = TestBed.createComponent(StutgartsmapComponent);
    cards.detectChanges(); map.detectChanges();
    http.expectOne(`${url}/stuttgart-listings`).flush(response);
    map.nativeElement.querySelector('#a101_Oberer_Schlossgarten_3_').dispatchEvent(new MouseEvent('click', { bubbles: true }));
    http.expectOne(req => req.url === `${url}/stuttgart-listings` && req.params.get('district') === 'Mitte')
      .flush({ ...response, listings: [] });
    cards.detectChanges();
    expect(cards.nativeElement.textContent).toContain('Keine Immobilien gefunden');
    cards.destroy(); map.destroy();
  });

  it('lists mapped districts with zero offers and selects them with a button', () => {
    const map = TestBed.createComponent(StutgartsmapComponent);
    map.detectChanges();
    http.expectOne(`${url}/stuttgart-listings`).flush(response);
    map.detectChanges();
    const rows = [...map.nativeElement.querySelectorAll('.district-legend li')] as HTMLElement[];
    expect(rows.map(row => [row.querySelector('.name')?.textContent?.trim(),
      row.querySelector('.count')?.textContent?.trim()])).toEqual([
        ['Mitte', '7'], ['Nord', '0'], ['Obertürkheim', '0']
      ]);
    rows[2].querySelector('button')!.click();
    map.detectChanges();
    expect(TestBed.inject(DistrictFilterService).getCurrentDistrict()).toBe('Obertürkheim');
    expect(map.nativeElement.querySelector('#a521_Obertürkheim_3_').classList.contains('selected')).toBe(true);
    expect(rows[2].querySelector('button')!.getAttribute('aria-pressed')).toBe('true');
    map.destroy();
  });

  it('restores the map selection after delayed mapping and component recreation', () => {
    TestBed.inject(DistrictFilterService).setSelectedDistrict('Mitte');
    const map = TestBed.createComponent(StutgartsmapComponent);
    map.detectChanges();
    expect(map.nativeElement.querySelector('#Layer_15 path.selected')).toBeNull();
    http.expectOne(`${url}/stuttgart-listings`).flush(response);
    map.detectChanges();
    expect(map.nativeElement.querySelector('#a101_Oberer_Schlossgarten_3_').classList.contains('selected')).toBe(true);
    map.destroy();
    const returnedMap = TestBed.createComponent(StutgartsmapComponent);
    returnedMap.detectChanges();
    http.expectNone(`${url}/stuttgart-listings`);
    expect(returnedMap.nativeElement.querySelector('#a101_Oberer_Schlossgarten_3_').classList.contains('selected')).toBe(true);
    TestBed.inject(DistrictFilterService).setSelectedDistrict(null);
    expect(returnedMap.nativeElement.querySelector('#Layer_15 path.selected')).toBeNull();
    returnedMap.destroy();
  });

  it('ignores hidden and boundary paths and accepts an active path child', () => {
    const map = TestBed.createComponent(StutgartsmapComponent);
    map.detectChanges();
    http.expectOne(`${url}/stuttgart-listings`).flush({ ...response,
      mapDistricts: { ...response.mapDistricts, a101_Oberer_Schlossgarten: 'Mitte',
        a101_Oberer_Schlossgarten_1_: 'Mitte' } });
    for (const id of ['a101_Oberer_Schlossgarten', 'a101_Oberer_Schlossgarten_1_']) {
      map.nativeElement.querySelector(`[id="${id}"]`).dispatchEvent(new MouseEvent('click', { bubbles: true }));
      expect(TestBed.inject(DistrictFilterService).getCurrentDistrict()).toBeNull();
    }
    const title = document.createElementNS('http://www.w3.org/2000/svg', 'title');
    map.nativeElement.querySelector('#a101_Oberer_Schlossgarten_3_').appendChild(title);
    title.dispatchEvent(new MouseEvent('click', { bubbles: true }));
    expect(TestBed.inject(DistrictFilterService).getCurrentDistrict()).toBe('Mitte');
    map.destroy();
  });

  it('shows unknown addresses separately without selecting a geographic district', () => {
    const map = TestBed.createComponent(StutgartsmapComponent);
    map.detectChanges();
    http.expectOne(`${url}/stuttgart-listings`).flush({ ...response,
      districtCounts: [...response.districtCounts, { district: 'Unknown', count: 2 }] });
    map.detectChanges();
    const row = map.nativeElement.querySelector('.district-legend li:last-child');
    expect(row.querySelector('.name').textContent.trim()).toBe('Ohne Bezirksangabe');
    row.querySelector('button').click();
    expect(TestBed.inject(DistrictFilterService).getCurrentDistrict()).toBe('Unknown');
    expect(map.nativeElement.querySelector('#Layer_15 path.selected')).toBeNull();
    map.destroy();
  });

  it('cancels stale requests and recovers after an API error', () => {
    const cards = TestBed.createComponent(RealEstateComponent);
    cards.detectChanges();
    http.expectOne(`${url}/stuttgart-listings`).flush(response);
    const filter = TestBed.inject(DistrictFilterService);
    filter.setSelectedDistrict('Mitte');
    const stale = http.expectOne(req => req.params.get('district') === 'Mitte');
    filter.setSelectedDistrict('Nord');
    expect(stale.cancelled).toBe(true);
    http.expectOne(req => req.params.get('district') === 'Nord').flush({}, { status: 500, statusText: 'Server error' });
    cards.detectChanges();
    expect(cards.nativeElement.textContent).toContain('konnten nicht geladen werden');
    filter.setSelectedDistrict('West');
    http.expectOne(req => req.params.get('district') === 'West').flush(response);
    cards.detectChanges();
    expect(cards.nativeElement.textContent).toContain('Apartment in Mitte');
    cards.destroy();
  });

  it('loads listing details directly by ID', () => {
    const detail = TestBed.createComponent(ListingDetailComponent);
    detail.detectChanges();
    http.expectOne(`${url}/listings/1`).flush(listing);
    detail.detectChanges();
    expect(detail.nativeElement.textContent).toContain('Apartment in Mitte');
    detail.destroy();
  });

  for (const status of [404, 500]) {
    it(`handles detail HTTP ${status}`, () => {
      const detail = TestBed.createComponent(ListingDetailComponent);
      detail.detectChanges();
      http.expectOne(`${url}/listings/1`).flush({}, { status, statusText: 'Error' });
      detail.detectChanges();
      expect(detail.componentInstance.notFound()).toBe(status === 404);
      expect(detail.componentInstance.error()).toBe(status === 500);
      detail.destroy();
    });
  }
});
