import { ClampMaxOptions } from '../../types/utils.types';

function clampMax(values: number[], options?: ClampMaxOptions) {

  const clampMaxOptions: Required<ClampMaxOptions> = {
    floor: options ? (options.floor ?? Number.MIN_VALUE) : Number.MIN_VALUE,
    ceil: options ? options.ceil ?? Number.MAX_VALUE : Number.MAX_VALUE,
  }

  const max = Math.max(...values);
  if (max > clampMaxOptions.floor) return clampMaxOptions.ceil;
  if (max < clampMaxOptions.floor) return clampMaxOptions.floor;
  return max;
}

export {clampMax};
