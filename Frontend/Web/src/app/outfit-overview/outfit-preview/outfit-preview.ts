import { Component } from '@angular/core';
import {OutfitImages} from './outfit-images/outfit-images';

@Component({
  selector: 'fp-outfit-preview',
  imports: [
    OutfitImages
  ],
  templateUrl: './outfit-preview.html',
  styleUrl: './outfit-preview.scss',
})
export class OutfitPreview {}
