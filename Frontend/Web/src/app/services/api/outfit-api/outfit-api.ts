import {inject, Injectable} from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
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
      clothing: outfit.clothing.map(c => c.id),
    }
    const formData = new FormData();
    formData.append('outfit', new Blob([JSON.stringify(outfitDto)], {type: 'application/json'}));
    outfit.images.forEach((file, index) => {
      formData.append(`file-${index}`, file.file);
    })
    const headers = new HttpHeaders({"enctype": "multipart/form-data"});
    return this.http.post<OutfitPostResponse>(`${this.apiUrl}/outfit`, formData, {headers: headers})
      .pipe(map(outfitResponse => this.convertDtoToOutfit(outfitResponse)));
  }

  getOutfits(): Observable<Outfit[]> {
    return this.http.get<{ outfits: OutfitPostResponse[] }>(`${this.apiUrl}/outfit`)
      .pipe(map(res => res.outfits.map(outfitResponse => this.convertDtoToOutfit(outfitResponse))));
  }

  private convertDtoToOutfit(outfit: OutfitPostResponse): Outfit {
    return ({
      ...outfit,
      images: outfit.images.map(image => this.apiHelper.hydrateImageDto(image)),
      clothing: outfit.clothing.map(c => ({...c, images: c.images.map(i => this.apiHelper.hydrateImageDto(i))})),
    } as Outfit)
  }
}
