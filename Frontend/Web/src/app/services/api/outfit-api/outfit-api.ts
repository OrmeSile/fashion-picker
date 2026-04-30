import {inject, Injectable} from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {UUID} from '../../../../types/shared.types';
import {LocalOutfit, Outfit, OutfitDTO, OutfitPostResponse} from '../../../../types/outfit.types';
import {map, Observable} from 'rxjs';
import {ApiHelper} from '../api-helper/api-helper';

@Injectable({
  providedIn: 'root',
})
export class OutfitApi {

  private readonly apiUrl = environment.backendUrl;
  private readonly http = inject(HttpClient);
  private readonly apiHelper = inject(ApiHelper);

  uploadOutfit(outfit: LocalOutfit): Observable<Outfit> {
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
          const cmsUrl = environment.fileRepositoryUrl;
          return {
            ...value,
            images: value.images.map(image => this.apiHelper.hydrateImageDto(image))
          } as Outfit
        }));
  }

  getOutfits(): Observable<Outfit[]> {
    return this.http.get<{outfits: OutfitPostResponse[]}>(`${this.apiUrl}/outfit`)
      .pipe(map(res => res.outfits.map(outfitResponse => this.ConvertDtoToOutfit(outfitResponse))));
  }

  private ConvertDtoToOutfit(outfit: OutfitPostResponse): Outfit {
    const cmsUrl = environment.fileRepositoryUrl;
    return ({
      ...outfit,
      images: outfit.images.map(image => {
        return {
          ...image,
          small: `${cmsUrl}${image.small}`,
          medium: image.medium ? `${cmsUrl}${image.medium}` : image.medium,
          large: image.large ? `${cmsUrl}${image.large}` : image.large,
          original: `${cmsUrl}${image.original}`,
        }
      })
    } as Outfit)
  }
}

type Clothing = {
  images?: string[],
  type: number,
  id?: UUID
}
