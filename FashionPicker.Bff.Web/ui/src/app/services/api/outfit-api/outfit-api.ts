import {inject, Injectable} from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {LocalOutfit, Outfit, OutfitDTO} from '../../../../types/outfit.types';
import {map, Observable} from 'rxjs';
import {ApiHelper} from '../api-helper/api-helper';
import {UUID} from '../../../../types/shared.types';
import {OutfitGetAllResponse, OutfitGetResponse, OutfitPostResponse} from '../../../../types/outfit.api.types';

@Injectable({
  providedIn: 'root',
})
export class OutfitApi {

  private readonly apiUrl = environment.backendUrl;
  private readonly http = inject(HttpClient);
  private readonly apiHelper = inject(ApiHelper);
  private readonly multipartHeaders = new HttpHeaders({"enctype": "multipart/form-data"});

  uploadOutfit(outfit: LocalOutfit): Observable<Outfit> {
    const formData = this.prepareOutfit(outfit);
    return this.http.post<OutfitPostResponse>(`${this.apiUrl}/outfit`, formData, {headers: this.multipartHeaders})
      .pipe(map(outfitResponse => this.hydrateOutfit(outfitResponse)));
  }

  editOutfit(outfit: LocalOutfit) {
    const formData = this.prepareOutfit(outfit);
    return this.http.put<OutfitPostResponse>(`${this.apiUrl}/outfit/${outfit.id}`, formData, {headers: this.multipartHeaders})
      .pipe(map(outfitResponse => this.hydrateOutfit(outfitResponse)));
  }

  getOutfitById(id: UUID): Observable<Outfit> {
    return this.http.get<OutfitGetResponse>(`${this.apiUrl}/outfit/${id}`)
      .pipe(map(res => this.hydrateOutfit(res.outfit)));
  }

  getOutfits(): Observable<Outfit[]> {
    return this.http.get<OutfitGetAllResponse>(`${this.apiUrl}/outfit`)
      .pipe(map(res => res.outfits.map(outfitResponse => this.hydrateOutfit(outfitResponse))));
  }

  private hydrateOutfit(outfit: Outfit): Outfit {
    return ({
      ...outfit,
      images: outfit.images.map(image => this.apiHelper.hydrateImageDto(image)),
      clothing: outfit.clothing.map(c => ({...c, images: c.images.map(i => this.apiHelper.hydrateImageDto(i))})),
    } as Outfit)
  }

  private prepareOutfit(outfit: LocalOutfit) {
    const outfitDto = this.toOutfitDto(outfit);
    return this.prepareFormData(outfitDto, outfit);
  }

  private toOutfitDto(outfit: LocalOutfit) {
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
      clothing: outfit.clothing,
    }
    return outfitDto;
  }

  private prepareFormData(outfitDto: OutfitDTO, outfit: LocalOutfit) {
    const formData = new FormData();
    formData.append('outfit', new Blob([JSON.stringify(outfitDto)], {type: 'application/json'}));
    outfit.images.forEach((file, index) => {
      formData.append(`file-${index}`, file.file);
    })
    return formData;
  }
}
