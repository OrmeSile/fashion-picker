import {Component, inject, OnInit} from '@angular/core';
import {OutfitMetadataForm} from '../../components/outfit-creation/outfit-metadata-form/outfit-metadata.form';
import {FileHandler} from '../../services/file-handler/file-handler';
import {OutfitMetadataFormData} from '../../../types/outfit.types';
import {DropZone} from '../../components/shared/drop-zone/drop-zone';
import {UUID} from '../../../types/shared.types';
import {ActivatedRoute, Router} from '@angular/router';
import {OutfitEdit} from '../../services/outfit-edit/outfit-edit';

@Component({
  selector: 'fp-outfit-edit-page',
  imports: [
    DropZone,
    OutfitMetadataForm,
    DropZone
  ],
  providers: [FileHandler, OutfitEdit],
  templateUrl: './outfit-edit.page.html',
  styleUrl: './outfit-edit.page.scss',
})
export class OutfitEditPage implements OnInit {

  private router = inject(Router);
  private activatedRoute = inject(ActivatedRoute);
  private outfitId: UUID | undefined = this.activatedRoute.snapshot.params['id'];

  protected outfitEditHandler = inject(OutfitEdit);

  ngOnInit(): void {
    if (!this.outfitId)
      return;
    this.outfitEditHandler.setOutfitId(this.outfitId);
  }

  protected handleDataTransfer(fileList: FileList) {
    this.outfitEditHandler.addImages(fileList);
  }

  protected async handleSubmitted(outfitMetadata: OutfitMetadataFormData) {
    this.outfitEditHandler.saveChanges(outfitMetadata).subscribe(() =>
      void this.router.navigate(['/'])
    );
  }
}
