import { Component, inject, computed, signal, DestroyRef } from '@angular/core';
import { OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { RealEstateService } from '../../services/realestate';
import { DistrictFilterService } from '../../services/district-filter';
import { Listing } from '../../interface/Listing';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

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

  allListings = signal<Listing[]>([]);
  selectedDistrict = signal<string | null>(null);

  filteredListings = computed(() => {
    const district = this.selectedDistrict();
    const listings = this.allListings();
    return district ? listings.filter((listing) => listing.mappedDistrict === district) : listings;
  });

  ngOnInit(): void {
    this.realEstateService
      .getStuttgartListings()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((data) => this.allListings.set(data));

    this.districtFilterService.selectedDistrict$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((district) => this.selectedDistrict.set(district));
  }
}
