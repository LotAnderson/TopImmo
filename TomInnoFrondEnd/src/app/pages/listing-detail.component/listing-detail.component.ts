import { Component, inject, signal, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { RealEstateService } from '../../services/realestate';
import { Listing } from '../../interface/Listing';

@Component({
  selector: 'app-listing-detail',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './listing-detail.component.html',
  styleUrl: './listing-detail.component.scss',
})
export class ListingDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private realEstateService = inject(RealEstateService);

  listing = signal<Listing | null>(null);
  notFound = signal(false);
  activeImageIndex = signal(0);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      this.notFound.set(true);
      return;
    }

    // erst im Cache suchen (kommt man von der Liste, ist das Objekt schon geladen)
    const cached = this.realEstateService.findCachedListing(id);
    if (cached) {
      this.listing.set(cached);
      return;
    }

    // sonst neu laden (z. B. nach einem Seiten-Reload direkt auf dieser URL)
    this.realEstateService.getStuttgartListings().subscribe((listings) => {
      const found = listings.find((item) => item.id === id);
      if (found) {
        this.listing.set(found);
      } else {
        this.notFound.set(true);
      }
    });
  }

  selectImage(index: number): void {
    this.activeImageIndex.set(index);
  }
}
