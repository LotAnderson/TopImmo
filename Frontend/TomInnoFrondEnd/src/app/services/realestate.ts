import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, shareReplay } from 'rxjs';
import { Listing } from '../interface/Listing';
import { ListingSearchResult } from '../interface/ListingSearchResult';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class RealEstateService {
  private apiUrl = `${environment.apiUrl}/realestate`;
  private http = inject(HttpClient);
  private overview$?: Observable<ListingSearchResult>;

  getStuttgartListings(district: string | null = null): Observable<ListingSearchResult> {
    if (district) {
      return this.http.get<ListingSearchResult>(`${this.apiUrl}/stuttgart-listings`, {
        params: new HttpParams().set('district', district),
      });
    }
    return this.overview$ ??= this.http
      .get<ListingSearchResult>(`${this.apiUrl}/stuttgart-listings`)
      .pipe(shareReplay({ bufferSize: 1, refCount: true }));
  }

  getListing(id: string): Observable<Listing> {
    return this.http.get<Listing>(`${this.apiUrl}/listings/${encodeURIComponent(id)}`);
  }
}
