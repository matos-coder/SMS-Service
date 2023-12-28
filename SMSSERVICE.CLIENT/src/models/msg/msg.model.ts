export interface IMessagGroupGetDto {

  id :string,
  groupName:string,
  groupCode:string,
  remark:string,
  organizationId:string,
  organizationName:string

}
export interface IMessagGroupPostDto {


  groupName:string,
  groupCode:string,
  remark:string,
  organizationId: string,
  createdById: string

}
export interface IMessagGroupPutDto {

  //Id:string,
  groupName:string,
  groupCode:string,
  remark:string,
  organizationId: string,
  createdById: string
  //organizationName:string


}
