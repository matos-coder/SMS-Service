export interface IPhoneGroupGetDto {

  id :string,
  messageGroupId:string,
  messageGroup:string,
  phoneNumber:string,
  fullName:string,
  remark:string

}
export interface IPhoneGroupPostDto {

  //id :string,
  messageGroupId:string,
  phoneNumber:string,
  fullName:string,
  remark:string,
  createdById:string

}
