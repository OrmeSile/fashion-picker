import {Directive, ElementRef, inject} from '@angular/core';

@Directive({
  selector: '[fpInnerDropShadowFilter]',
})
export class InnerDropShadowFilter {
  private svgEl = inject<ElementRef<SVGElement>>(ElementRef);


  constructor() {
    const filter = document.createElementNS('http://www.w3.org/2000/svg', 'filter');

  }
}
