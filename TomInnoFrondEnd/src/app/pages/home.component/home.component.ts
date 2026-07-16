import { Component } from '@angular/core';
import { StutgartsmapComponent } from '../stutgartsmap.component/stutgartsmap.component';
import { RealEstateComponent } from '../../component/realestate.component/realestate.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [StutgartsmapComponent, RealEstateComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {}
