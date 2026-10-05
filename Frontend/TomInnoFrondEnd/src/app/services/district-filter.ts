import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Observable } from 'rxjs/internal/Observable';


@Injectable({
  providedIn: 'root',
})
export class DistrictFilterService {
  private _selectedDistrict$ = new BehaviorSubject<string | null>(null);
  public selectedDistrict$: Observable<string | null> = this._selectedDistrict$.asObservable();

  setSelectedDistrict(district: string | null):void {
    this._selectedDistrict$.next(district);
  }
  public getCurrentDistrict(): string | null {
    return this._selectedDistrict$.getValue();
  }
}
