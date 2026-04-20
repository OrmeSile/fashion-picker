import {inject, Injectable} from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {FileInformation, UUID} from '../../../../types/shared.types';

@Injectable({
  providedIn: 'root',
})
export class OutfitApi {

  private apiUrl = environment.fileRepositoryUrl;
  private http = inject(HttpClient);

  uploadClothing(files: File[] ){
    const formData = new FormData();
    formData.append('clothing', new Blob([JSON.stringify({type: 1})], { type: 'application/json' }));
    files.forEach((file, index) => {
      formData.append(`file-${index}`, file);
    })
    const headers = new HttpHeaders({"enctype": "multipart/form-data"});
    return this.http.post(`${this.apiUrl}/clothing`, formData, {headers: headers});
  }
}

type Clothing = {
  images?: string[],
  type: number,
  id?: UUID
}
