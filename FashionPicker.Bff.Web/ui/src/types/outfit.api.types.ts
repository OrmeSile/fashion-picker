import {Outfit} from './outfit.types';

type OutfitPostResponse = Outfit & {};
interface OutfitGetResponse {outfit: Outfit}
interface OutfitGetAllResponse {outfits: Outfit[]}

export type {OutfitPostResponse, OutfitGetResponse, OutfitGetAllResponse};
