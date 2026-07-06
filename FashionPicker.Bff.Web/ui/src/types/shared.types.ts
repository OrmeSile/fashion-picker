import {ImageFile} from './files.types';

type UUID = ReturnType<typeof crypto.randomUUID>;

interface FileInformation {
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

interface ImageDto {
  small: string,
  medium?: string,
  big?: string,
  original: string,
  mimeType: string,
}

interface FileState {
  file: ImageFile,
  loading: boolean,
  success: boolean
}

interface AlertMessage {
  id: number,
  type: "information" | "error" | "warning",
  text: string,
}

export type {UUID, FileInformation, ImageDto, FileState, AlertMessage};
