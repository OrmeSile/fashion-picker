import { Component } from '@angular/core';
import {OutfitMetadataForm} from '../../components/outfit-creation/outfit-metadata-form/outfit-metadata.form';

@Component({
  selector: 'fp-test-bed-page',
  imports: [
    OutfitMetadataForm
  ],
  templateUrl: './test-bed.page.html',
  styleUrl: './test-bed.page.scss',
})
export class TestBedPage {}
