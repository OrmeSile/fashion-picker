import {WritableSignal} from '@angular/core';

type UUID = ReturnType<typeof crypto.randomUUID>;

type FileInformation = {
  physicalFileName: string;
  logicalfileName: string;
  pathSmall: string;
  pathMedium: string;
  pathBig: string;
  pathOriginal: string;
  mimeType: string;
  tags: string[];
  extension: string;
}

type ImageDto = {
  small: string,
  medium?: string,
  big?: string,
  original: string,
  mimeType: string,
}
export type {UUID, FileInformation, ImageDto };
