import { Injectable } from '@angular/core';
import {ImageDto} from '../../../../types/shared.types';
import {environment} from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiHelper {
  private readonly backendUrl = environment.cmsUrl;

  hydrateImageDto(dto: ImageDto): ImageDto {
    return {
      ...dto,
      small: `${this.backendUrl}/${dto.small}`,
      medium: dto.medium ? `${this.backendUrl}/${dto.medium}` : dto.medium,
      large: dto.large ? `${this.backendUrl}/${dto.large}` : dto.large,
      original: `${this.backendUrl}/${dto.original}`,
    }
  }
}
