import {Component, input, model} from '@angular/core';
import {FormCheckboxControl} from '@angular/forms/signals';
import {Season} from '../../../../types/outfit.types';
import {LeafIcon} from '../../icons/leaf-icon/leaf.icon';
import {AutumnLeafIcon} from '../../icons/autumn-leaf.icon/autumn-leaf.icon';
import {SnowflakeIcon} from '../../icons/snowflake.icon/snowflake.icon';
import {SummerSunImageIcon} from '../../icons/summer-sun-image.icon/summer-sun-image.icon';

@Component({
  selector: 'fp-season-control',
  imports: [
    LeafIcon,
    AutumnLeafIcon,
    SnowflakeIcon,
    SummerSunImageIcon,
  ],
  templateUrl: './season.control.html',
  styleUrl: './season.control.scss',
  host:{
    '[attr.data-season]': 'season()'
  }
})

export class SeasonControl implements FormCheckboxControl {
  readonly checked = model(false);
  readonly label = input.required<string>();
  readonly season = input.required<Season>();

  toggle(){
    this.checked.update((val) => !val);
  }

}
