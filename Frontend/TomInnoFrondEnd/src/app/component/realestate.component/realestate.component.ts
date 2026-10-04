import { Component, inject, signal, DestroyRef } from '@angular/core';
import { OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RealEstateService } from '../../services/realestate';
import { DistrictFilterService } from '../../services/district-filter';
import { Listing } from '../../interface/Listing';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { catchError, of, switchMap } from 'rxjs';

@Component({
  selector: 'app-real-estate',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './realestate.component.html',
  styleUrls: ['./realestate.component.scss'],
})
export class RealEstateComponent implements OnInit {
  private realEstateService = inject(RealEstateService);
  private districtFilterService = inject(DistrictFilterService);
  private destroyRef = inject(DestroyRef);

  filteredListings = signal<Listing[]>([]);
  loading = signal(true);
  error = signal(false);

  ngOnInit(): void {
    this.districtFilterService.selectedDistrict$
      .pipe(
        switchMap((district) => {
          this.loading.set(true);
          this.error.set(false);
          return this.realEstateService.getStuttgartListings(district).pipe(
            catchError(() => {
              this.error.set(true);
              return of(null);
            }),
          );
        }),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe((result) => {
        this.filteredListings.set(result?.listings ?? []);
        this.loading.set(false);
      });
  }
}
