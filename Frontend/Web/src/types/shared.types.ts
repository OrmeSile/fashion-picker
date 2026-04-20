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

export type { UUID, FileInformation };
