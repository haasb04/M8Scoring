import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatchSetComponent } from './match-set.component';

describe('MatchSetComponent', () => {
  let component: MatchSetComponent;
  let fixture: ComponentFixture<MatchSetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MatchSetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MatchSetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
