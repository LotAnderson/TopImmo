import { Component, inject } from '@angular/core';
import { OnInit } from '@angular/core';
import { RealEstateService } from '../../services/realestate';
import { DistrictFilterService } from '../../services/district-filter';
import { Listing } from '../../interface/Listing';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-real-estate',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './realestate.component.html',
  styleUrls: ['./realestate.component.scss'],
})
export class RealEstateComponent implements OnInit {
  private realEstateService = inject(RealEstateService);
  private districtFilterService = inject(DistrictFilterService);
  allListings: Listing[] = [];
  filteredListings: Listing[] = [];

  private applyFilter(district: string | null): void {
    if (!district) {
      this.filteredListings = [...this.allListings];
    } else {
      this.filteredListings = this.allListings.filter(
        (listing) => listing.mappedDistrict === district,
      ); //помоги я не понял
    }
  }

  ngOnInit(): void {
    this.realEstateService.getStuttgartListings().subscribe((data) => {
      this.allListings = data;
      this.applyFilter(this.districtFilterService.getCurrentDistrict());
    });
    this.districtFilterService.selectedDistrict$.subscribe((district) => {
      this.applyFilter(district);
    });
  }
}
