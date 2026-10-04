import { Component, inject, Renderer2, ElementRef, OnInit, signal, DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { CommonModule } from '@angular/common';
import { DistrictFilterService } from '../../services/district-filter';
import { RealEstateService } from '../../services/realestate';

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
  private mapDistricts: Record<string, string> = {};
  selectedDistrict = signal<string | null>(null);

  ngOnInit(): void {
    this.realEstateService
      .getStuttgartListings()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (result) => {
          this.districtCounts.set(result.districtCounts);
          this.mapDistricts = result.mapDistricts;
        },
        error: () => this.districtCounts.set([]),
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
    const bezirk = this.mapDistricts[clickedId];
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
    if(this.mapDistricts[el.id] === bezirk) {
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

  const bezirk = this.mapDistricts[clickedId];

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
