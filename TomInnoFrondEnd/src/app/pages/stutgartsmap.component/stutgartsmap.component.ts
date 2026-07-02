import { Component, inject,Renderer2, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DistrictFilterService } from '../../services/district-filter';
import { getBezirkFromStadtteilId } from './district.models';


@Component({
  selector: 'app-stutgartsmap',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './stutgartsmap.component.html',
  styleUrl: './stutgartsmap.component.scss',
})
export class StutgartsmapComponent {

  private districtFilterService = inject(DistrictFilterService);
  private renderer = inject(Renderer2);
  private elementRef = inject(ElementRef);

//click on the map
  onMapClick(event: MouseEvent): void {
    const target = event.target as SVGElement;
    const clickedId = target.id;
    if (!clickedId || !clickedId.startsWith('a')) {
            return;
    }
    const bezirk = getBezirkFromStadtteilId(clickedId);
    if (bezirk) {
      this.districtFilterService.setSelectedDistrict(bezirk);
      this.highlightBezirk(bezirk, 'selected');
    }
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



