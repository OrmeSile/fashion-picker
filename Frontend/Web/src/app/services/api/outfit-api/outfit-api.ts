import {inject, Injectable} from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {UUID} from '../../../../types/shared.types';
import {LocalOutfit, OutfitDTO, OutfitPostResponse} from '../../../../types/outfit.types';
import {map} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OutfitApi {

  private apiUrl = environment.backendUrl;
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

  uploadOutfit(outfit: LocalOutfit) {
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
    return this.http.post<OutfitPostResponse>(`${this.apiUrl}/outfit`, formData, {headers: headers})
      .pipe(
        map(value => {
          console.log(value)
          const cmsUrl = environment.fileRepositoryUrl;
          return ({
            ...value,
              images: value.images.map(image => {
                return {
                  ...image,
                  small: `${cmsUrl}${image.small}`,
                  medium: image.medium ? `${cmsUrl}${image.medium}`: image.medium,
                  large: image.large ? `${cmsUrl}${image.large}` : image.large,
                  original: `${cmsUrl}${image.original}`,
                }
              })
            } as OutfitPostResponse)
        }));
  }
}

type Clothing = {
  images?: string[],
  type: number,
  id?: UUID
}
