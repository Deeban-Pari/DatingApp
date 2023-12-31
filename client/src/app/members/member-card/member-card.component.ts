import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
})
export class MemberCardComponent implements OnInit {
  @Input() member: Member | undefined;
  constructor() {}
  ngOnInit(): void {}
}
