import {Outfit} from './outfit.types';

type OutfitPostResponse = Outfit & {};
type OutfitGetResponse = {outfit: Outfit};
type OutfitGetAllResponse = {outfits: Outfit[]};

export type {OutfitPostResponse, OutfitGetResponse, OutfitGetAllResponse};
