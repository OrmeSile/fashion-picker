import {UUID} from './shared.types';

type Outfit = {
  id: UUID,
  name: string,
  description: string,
  season: string,
  colors: string[],
  tags: string[],
};

export type {Outfit};
