import {Component, model, output} from '@angular/core';
import {FormValueControl} from '@angular/forms/signals';

@Component({
  selector: 'fp-outfit-tag',
  imports: [],
  templateUrl: './outfit-tag.html',
  styleUrl: './outfit-tag.scss',
})



export class OutfitTag implements FormValueControl<string>{
  value = model('');
  deleteCLicked = output<void>();

  protected handleInput(event: Event) {
    const target = event.target as HTMLInputElement;
    this.value.set(target.value);
  }

  protected handleKeyInput(event: KeyboardEvent) {
    if(event.key === 'Enter') {
      console.log('Enter');
    }

    if(event.key === 'Escape') {
      console.log('Escape');
    }
  }

  protected delete() {
    this.deleteCLicked.emit();
  }
}
