import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, shareReplay, tap } from 'rxjs';
import { Listing } from '../interface/Listing';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RealEstateService {

  private apiUrl = `${environment.apiUrl}/realestate/stuttgart-listings`;

  private http = inject(HttpClient);

  // letzte geladene Liste zwischenspeichern, damit die Detailseite Objekte per id finden kann
  private lastListings: Listing[] = [];

  // Mehrere Komponenten (Liste + Karte) brauchen dieselben Daten —
  // shareReplay sorgt dafür, dass nur eine HTTP-Anfrage gestellt wird.
  private listings$?: Observable<Listing[]>;

  getStuttgartListings(): Observable<Listing[]> {
    if (!this.listings$) {
      this.listings$ = this.http.get<Listing[]>(this.apiUrl).pipe(
        tap((listings) => (this.lastListings = listings)),
        shareReplay(1),
      );
    }
    return this.listings$;
  }

  findCachedListing(id: string): Listing | undefined {
    return this.lastListings.find((listing) => listing.id === id);
  }
}