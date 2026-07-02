import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { StutgartsmapComponent } from './pages/stutgartsmap.component/stutgartsmap.component';
import { RealEstateComponent } from './component/realestate.component/realestate.component';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, StutgartsmapComponent, RealEstateComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('TomInnoFrondEnd');
}
