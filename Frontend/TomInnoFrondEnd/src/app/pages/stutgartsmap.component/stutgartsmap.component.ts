import { Component, inject, Renderer2, ElementRef, OnInit, signal, DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { CommonModule } from '@angular/common';
import { DistrictFilterService } from '../../services/district-filter';
import { RealEstateService } from '../../services/realestate';
import { getBezirkFromStadtteilId } from './district.models';

interface DistrictCount {
  district: string;
  count: number;
}

@Component({
  selector: 'app-stutgartsmap',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './stutgartsmap.component.html',
  styleUrl: './stutgartsmap.component.scss',
})
export class StutgartsmapComponent implements OnInit {

  private districtFilterService = inject(DistrictFilterService);
  private realEstateService = inject(RealEstateService);
  private renderer = inject(Renderer2);
  private elementRef = inject(ElementRef);
  private destroyRef = inject(DestroyRef);

  // Anzahl Mietwohnungen je Stadtteil, für die Legende neben der Karte
  districtCounts = signal<DistrictCount[]>([]);
  selectedDistrict = signal<string | null>(null);

  ngOnInit(): void {
    this.realEstateService
      .getStuttgartListings()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((listings) => {
        const counts = new Map<string, number>();
        for (const listing of listings) {
          const isMietwohnung = listing.realEstateType?.toLowerCase() === 'apartmentrent';
          if (!isMietwohnung || !listing.mappedDistrict) {
            continue;
          }
          counts.set(listing.mappedDistrict, (counts.get(listing.mappedDistrict) ?? 0) + 1);
        }
        const sorted = [...counts.entries()]
          .map(([district, count]) => ({ district, count }))
          .sort((a, b) => b.count - a.count);
        this.districtCounts.set(sorted);
      });

    this.districtFilterService
      .selectedDistrict$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((district) => this.selectedDistrict.set(district));
  }

//click on the map
  onMapClick(event: MouseEvent): void {
    const target = event.target as SVGElement;
    const clickedId = target.id;
    if (!clickedId || !clickedId.startsWith('a')) {
            return;
    }
    const bezirk = getBezirkFromStadtteilId(clickedId);
    if (bezirk) {
      this.selectBezirk(bezirk);
    }
  }

  // Klick auf einen Eintrag der Legende neben der Karte
  onLegendClick(bezirk: string): void {
    this.selectBezirk(bezirk);
  }

  private selectBezirk(bezirk: string): void {
    this.districtFilterService.setSelectedDistrict(bezirk);
    this.highlightBezirk(bezirk, 'selected');
  }

 //highlight the selected district on the map
  highlightBezirk(bezirk: string, className:string): void {
  const allPaths = this.elementRef.nativeElement.querySelectorAll('#Layer_15 path');

  allPaths.forEach((el: SVGElement) => {
    this.renderer.removeClass(el, className);
    if(getBezirkFromStadtteilId(el.id) === bezirk) {
      this.renderer.addClass(el, className);
    }
  });
}

//hover on the map
onMapMouseOver(event: MouseEvent): void {
  const target = event.target as SVGElement;
  const clickedId = target.id;

  if (!clickedId || !clickedId.startsWith('a')) {
    return;
  }

  const bezirk = getBezirkFromStadtteilId(clickedId);

  if (bezirk) {
    this.highlightBezirk(bezirk, 'hovered');
  }
}
//hover out on the map
onMapMouseOut(event: MouseEvent): void {
  const allPaths = this.elementRef.nativeElement.querySelectorAll('#Layer_15 path');
  allPaths.forEach((el: SVGElement) => {
    this.renderer.removeClass(el, 'hovered');
  });
}

}
