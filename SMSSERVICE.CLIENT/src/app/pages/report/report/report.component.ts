import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService, SelectItem } from 'primeng/api';
import { MessageGroupService } from 'src/app/services/message-group.service';
import { SendMessageService } from 'src/app/services/send-message.service';
import { UserService } from 'src/app/services/user.service';
import { UserView } from 'src/models/auth/userDto';
import { IMessagGroupGetDto } from 'src/models/msg/msg.model';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent {
  user !: UserView
  messagegroups:IMessagGroupGetDto[]
  groupOptions: SelectItem[] = []

  constructor(private modalService:NgbModal,
    private messageService:MessageService,
    private userService:UserService,
    private msgService: MessageGroupService,
    private sndMessage: SendMessageService){}

  ngOnInit(): void {
    this.user= this.userService.getCurrentUser()
    this.getGroup(this.user.organizationId)
  }

  getGroup(Organization: string): void {
    //this.isFirstSelectOptionSelected = true;
    this.msgService.getMessageGroups(Organization).subscribe({
      next: (res) => {
        this.messagegroups = res;
        this.groupOptions = this.messagegroups.map(group => ({
          value: group.id,
          label: group.groupName
        }));
        console.log(this.messagegroups);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}
