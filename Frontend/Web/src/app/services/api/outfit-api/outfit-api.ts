import {inject, Injectable} from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {FileInformation, UUID} from '../../../../types/shared.types';
import {Outfit, OutfitDTO} from '../../../../types/outfit.types';

@Injectable({
  providedIn: 'root',
})
export class OutfitApi {

  private apiUrl = environment.backendUrl;
  private cmsUrl = environment.fileRepositoryUrl;
  private http = inject(HttpClient);

  uploadClothing(files: File[]) {
    const formData = new FormData();
    formData.append('clothing', new Blob([JSON.stringify({type: 1})], {type: 'application/json'}));
    files.forEach((file, index) => {
      formData.append(`file-${index}`, file);
    })
    const headers = new HttpHeaders({"enctype": "multipart/form-data"});
    return this.http.post(`${this.apiUrl}/outfit`, formData, {headers: headers});
  }

  uploadOutfit(outfit: Outfit) {
    const outfitDto: OutfitDTO = {
      id: outfit.id,
      colors: outfit.colors,
      seasons: outfit.seasons
               ? Object.entries(outfit.seasons)
                 .reduce<string[]>((acc, [k, v]) => v ? [k, ...acc] : acc, [])
               : [],
      mood: outfit.mood,
      sport: outfit.sport,
      tags: outfit.tags,
    }
    const formData = new FormData();
    formData.append('outfit', new Blob([JSON.stringify(outfitDto)], {type: 'application/json'}));
    outfit.images.forEach((file, index) => {
      formData.append(`file-${index}`, file.file);
    })
    const headers = new HttpHeaders({"enctype": "multipart/form-data"});
    return this.http.post(`${this.apiUrl}/outfit`, formData, {headers: headers});
  }
}

type Clothing = {
  images?: string[],
  type: number,
  id?: UUID
}
