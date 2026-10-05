import { Component, inject, signal, OnInit, DestroyRef } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { RealEstateService } from '../../services/realestate';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
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

  private destroyRef = inject(DestroyRef);
  error = signal(false);
  listing = signal<Listing | null>(null);
  notFound = signal(false);
  activeImageIndex = signal(0);

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      this.notFound.set(true);
      return;
    }

    this.realEstateService.getListing(id)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (listing) => this.listing.set(listing),
        error: (error) => {
          if (error.status === 404) this.notFound.set(true);
          else this.error.set(true);
        },
      });
  }

  selectImage(index: number): void {
    this.activeImageIndex.set(index);
  }
}
