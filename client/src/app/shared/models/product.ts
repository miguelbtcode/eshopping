import { IBrand } from "./brand"
import { IType } from "./type"

export interface IProduct {
  id: string
  name: string
  summary: string
  description: string
  imageFile: string
  brand: IBrand
  type: IType
  price: number
}
