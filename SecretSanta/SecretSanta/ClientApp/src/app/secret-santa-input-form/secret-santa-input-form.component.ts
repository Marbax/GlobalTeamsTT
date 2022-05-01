import { Component, EventEmitter, Output } from '@angular/core'
import { FormGroup, FormControl, Validators } from '@angular/forms'

@Component({
    selector: 'app-secret-santa-input-form',
    templateUrl: './secret-santa-input-form.component.html',
    styleUrls: ['./secret-santa-input-form.component.css'],
})
export class SecretSantaInputFormComponent {
    takeAPartSantaForm = new FormGroup({
        firstName: new FormControl('', Validators.required),
        lastName: new FormControl('', Validators.required),
    })

    @Output() onSubmit: EventEmitter<UserData> = new EventEmitter();
    
    constructor() {}

    handleSubmit(): void {
        this.onSubmit.emit(this.takeAPartSantaForm.value);
        this.takeAPartSantaForm.reset()
    }
}

interface UserData{
  firstName: string;
  lastName: string;
}
