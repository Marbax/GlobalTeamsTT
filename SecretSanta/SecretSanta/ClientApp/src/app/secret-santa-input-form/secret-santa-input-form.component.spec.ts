import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SecretSantaInputFormComponent } from './secret-santa-input-form.component';

describe('SecretSantaInputFormComponent', () => {
  let component: SecretSantaInputFormComponent;
  let fixture: ComponentFixture<SecretSantaInputFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SecretSantaInputFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SecretSantaInputFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
