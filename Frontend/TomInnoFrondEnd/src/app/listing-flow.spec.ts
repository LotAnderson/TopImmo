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
  mapDistricts: { a101_Oberer_Schlossgarten: 'Mitte' } };

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
    map.nativeElement.querySelector('#a101_Oberer_Schlossgarten').dispatchEvent(new MouseEvent('click', { bubbles: true }));
    http.expectOne(req => req.url === `${url}/stuttgart-listings` && req.params.get('district') === 'Mitte')
      .flush({ ...response, listings: [] });
    cards.detectChanges();
    expect(cards.nativeElement.textContent).toContain('Keine Immobilien gefunden');
    cards.destroy(); map.destroy();
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
