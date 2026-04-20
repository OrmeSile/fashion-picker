import {computed, Injectable, signal} from '@angular/core';
import {OutfitFile} from '../../../types/files.types';

@Injectable({
  providedIn: 'root',
})
export class FileHandler {

  files = signal<OutfitFile[]>([]);

  addFiles(fileList: FileList) {
    for (const file of fileList) {
      if(file.type === 'image/jpeg' || file.type === 'image/png'){
        const newId = crypto.randomUUID();
        const newFile = {
          id: newId,
          file,
          fileUrl: URL.createObjectURL(file)
        }

        this.files.update(files => [...files, newFile]);
      }
    }
  }

  removeFile(id: ReturnType<typeof crypto.randomUUID>) {
    this.files.update(files => files.filter(fileData => fileData.id !== id));
  }
}
