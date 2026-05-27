import {inject, Injectable} from '@angular/core';
import {environment} from '../../../../environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Clothing, ClothingDto, ClothingGetAllResponse, LocalClothing} from '../../../../types/clothing.types';
import {map} from 'rxjs';
import {ApiHelper} from '../api-helper/api-helper';

@Injectable({
  providedIn: 'root',
})
export class ClothingApi {
  private apiUrl = environment.backendUrl;

  private readonly http = inject(HttpClient);
  private readonly apiHelper = inject(ApiHelper);

  uploadClothing(clothing: LocalClothing) {
    const formData = new FormData();
    const clothingDto = {
      clothingType: clothing.clothingType
    };
    formData.append('clothing', new Blob([JSON.stringify(clothingDto)], {type: 'application/json'}));
    clothing.files.forEach((file, index) => {
      formData.append(`file-${index}`, file);
    })
    const headers = new HttpHeaders({"enctype": "multipart/form-data"});
    return this.http.post<ClothingDto>(`${this.apiUrl}/clothing`, formData, {headers: headers})
      .pipe(map((res) => {
        return {
          ...res,
          images: res.images.map(image => this.apiHelper.hydrateImageDto(image))
        } as Clothing
      }))
      ;
  }

  getAllClothing() {
    return this.http.get<ClothingGetAllResponse>(`${this.apiUrl}/clothing`)
      .pipe(
        map((res) => {
          return {
            clothing: res.clothing.map(clothing => ({
              ...clothing, images: clothing.images.map(image => this.apiHelper.hydrateImageDto(image))
            }))
          }
        }));

  }
}
