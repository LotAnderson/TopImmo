import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Listing } from '../interface/Listing';

@Injectable({
  providedIn: 'root'
})
export class RealEstateService {
 
  private apiUrl = 'https://localhost:7246/api/realestate/stuttgart-listings';

  private http = inject(HttpClient);

  getStuttgartListings(): Observable<Listing[]> {
    return this.http.get<Listing[]>(this.apiUrl);
  }
}