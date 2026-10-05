import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { StutgartsmapComponent } from './pages/stutgartsmap.component/stutgartsmap.component';
import { environment } from '../environments/environment';

function contains(polygon: Element, x: number, y: number): boolean {
  const coordinates = polygon.getAttribute('points')!.match(/-?\d+(?:\.\d+)?/g)!.map(Number);
  let inside = false;
  for (let i = 0, j = coordinates.length - 2; i < coordinates.length; j = i, i += 2) {
    const ax = coordinates[i], ay = coordinates[i + 1];
    const bx = coordinates[j], by = coordinates[j + 1];
    if ((ay > y) !== (by > y) && x < (bx - ax) * (y - ay) / (by - ay) + ax) inside = !inside;
  }
  return inside;
}

describe('Birkach and Plieningen district outlines', () => {
  it('encloses all three Birkach Stadtteile and keeps all five Plieningen Stadtteile outside Birkach', () => {
    TestBed.configureTestingModule({ providers: [provideHttpClient(), provideHttpClientTesting()] });
    const fixture = TestBed.createComponent(StutgartsmapComponent);
    fixture.detectChanges();
    const http = TestBed.inject(HttpTestingController);
    http.expectOne(`${environment.apiUrl}/realestate/stuttgart-listings`)
      .flush({ listings: [], districtCounts: [], mapDistricts: {} });
    const birkach = fixture.nativeElement.querySelector('#bezirk-Birkach');
    const plieningen = fixture.nativeElement.querySelector('#bezirk-Plieningen');
    // Interior coordinates from the existing Stadtteil shapes, independent
    // of the district outlines. Birkach-Süd was outside the old Birkach outline.
    const places = [
      { name: 'Birkach-Nord', x: 452.11, y: 578.47, inBirkach: true },
      { name: 'Birkach-Süd', x: 439.63, y: 599.66, inBirkach: true },
      { name: 'Schönberg', x: 446.6, y: 553.78, inBirkach: true },
      { name: 'Plieningen', x: 424.25, y: 667.17, inBirkach: false },
      { name: 'Chausseefeld', x: 432.59, y: 629.06, inBirkach: false },
      { name: 'Steckfeld', x: 443.36, y: 626.58, inBirkach: false },
      { name: 'Asemwald', x: 418.56, y: 587.04, inBirkach: false },
      { name: 'Hohenheim', x: 474.62, y: 639, inBirkach: false },
    ];
    for (const place of places) {
      expect(contains(birkach, place.x, place.y), place.name).toBe(place.inBirkach);
      expect(contains(plieningen, place.x, place.y), place.name).toBe(!place.inBirkach);
    }
    http.verify();
    fixture.destroy();
  });
});
