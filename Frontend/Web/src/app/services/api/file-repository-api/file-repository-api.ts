import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class FileRepositoryApi {

  private http = inject(HttpClient);

  uploadImage(file: File){
    const formData = new FormData();
    formData.append('clothing', file);
  }
}
