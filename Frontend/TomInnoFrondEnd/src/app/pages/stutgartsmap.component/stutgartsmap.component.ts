import { Component, computed, inject, Renderer2, ElementRef, OnInit, signal, DestroyRef } from '@angular/core';
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

  districtCounts = signal<DistrictCount[]>([]);
  private mapDistricts = signal<Record<string, string>>({});
  selectedDistrict = signal<string | null>(null);

  // Auch Bezirke ohne Angebot bleiben auswählbar. Unbestimmte Adressen
  // sind eine eigene Ergebnisgruppe und gehören zu keiner Kartenfläche.
  legendDistricts = computed(() => {
    const counts = new Map(this.districtCounts().map(item => [item.district, item.count]));
    const districts = [...new Set(Object.values(this.mapDistricts()))]
      .sort((a, b) => a.localeCompare(b, 'de'));
    if (counts.has('Unknown')) districts.push('Unknown');
    return districts.map(district => ({ district, count: counts.get(district) ?? 0 }));
  });

  ngOnInit(): void {
    this.realEstateService.getStuttgartListings()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (result) => {
          this.districtCounts.set(result.districtCounts);
          this.mapDistricts.set(result.mapDistricts);
          this.highlightBezirk(this.selectedDistrict(), 'selected');
        },
        error: () => this.districtCounts.set([]),
      });

    this.districtFilterService.selectedDistrict$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((district) => {
        this.selectedDistrict.set(district);
        this.highlightBezirk(district, 'selected');
      });
  }

  onMapClick(event: MouseEvent): void {
    const district = this.districtFromEvent(event);
    if (district) this.onLegendClick(district);
  }

  onLegendClick(district: string): void {
    this.districtFilterService.setSelectedDistrict(district);
  }

  private districtFromEvent(event: MouseEvent): string | undefined {
    if (!(event.target instanceof Element)) return undefined;
    const path = event.target.closest('#Layer_15 path');
    return path ? this.mapDistricts()[path.id] : undefined;
  }

  private highlightBezirk(district: string | null, className: string): void {
    const allPaths = this.elementRef.nativeElement.querySelectorAll('#Layer_15 path');
    allPaths.forEach((path: SVGElement) => {
      this.renderer.removeClass(path, className);
      if (district && this.mapDistricts()[path.id] === district) {
        this.renderer.addClass(path, className);
      }
    });
  }

  onMapMouseOver(event: MouseEvent): void {
    this.highlightBezirk(this.districtFromEvent(event) ?? null, 'hovered');
  }

  onMapMouseOut(_event: MouseEvent): void {
    this.highlightBezirk(null, 'hovered');
  }
}
