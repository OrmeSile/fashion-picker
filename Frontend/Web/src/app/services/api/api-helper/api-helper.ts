import { Injectable } from '@angular/core';
import {ImageDto} from '../../../../types/shared.types';
import {environment} from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiHelper {
  private readonly cmsUrl = environment.fileRepositoryUrl;

  hydrateImageDto(dto: ImageDto): ImageDto {
    return {
      ...dto,
      small: `${this.cmsUrl}${dto.small}`,
      medium: dto.medium ? `${this.cmsUrl}${dto.medium}` : dto.medium,
      large: dto.large ? `${this.cmsUrl}${dto.large}` : dto.large,
      original: `${this.cmsUrl}${dto.original}`,
    }
  }
}
