import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService } from 'primeng/api';
import { MessageGroupService } from 'src/app/services/message-group.service';
import { UserService } from 'src/app/services/user.service';
import { UserView } from 'src/models/auth/userDto';
import { IMessagGroupGetDto, } from 'src/models/msg/msg.model';

@Component({
  selector: 'app-update-group',
  templateUrl: './update-group.component.html',
  styleUrls: ['./update-group.component.scss']
})
export class UpdateGroupComponent implements OnInit {
  @Input() messagegroup: IMessagGroupGetDto
  messagegroupsForm!: FormGroup;
  user !: UserView
  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private messageService: MessageService,
    private activeModal : NgbActiveModal,
    private msgService : MessageGroupService
    ) { }
    ngOnInit(): void {

      this.user = this.userService.getCurrentUser()

      this.messagegroupsForm = this.formBuilder.group({
        groupName: [this.messagegroup.groupName, Validators.required],
        groupCode: [this.messagegroup.groupCode, Validators.required],
        remark: [this.messagegroup.remark, Validators.required]


      });
    }

    onSubmit() {

      console.log(this.messagegroupsForm.value)
      if (this.messagegroupsForm.valid) {
        const value : IMessagGroupGetDto = {
          id:this.user.userId,
          groupName: this.messagegroupsForm.value.GroupName,
          groupCode: this.messagegroupsForm.value.GroupCode,
          remark: this.messagegroupsForm.value.Remark,
          organizationId: this.user.organizationId,
          organizationName: this.user.fullName


        }
        this.msgService.updateMessageGroup(value).subscribe({
          next: (res) => {

            if (res.success) {
              this.messageService.add({ severity: 'success', summary: 'Successfull', detail: res.message });

              this.messagegroupsForm.reset();

              this.closeModal()


            }
            else {
              this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: res.message });

            }

          }, error: (err) => {
            this.messageService.add({ severity: 'error', summary: 'Something went Wrong', detail: err });
          }
        })



      }
      else {
        this.messageService.add({ severity: 'error', summary: 'Form Submit failed.', detail: "Please fil required inputs !!" });
      }


    }


    closeModal(){

      this.activeModal.close()
    }
}
