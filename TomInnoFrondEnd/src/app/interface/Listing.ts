import { Address } from "./Address";
import { AttributeItem } from "./AttributeItem";


export interface Listing {
  id: string;
  url: string;
  title: string;
  price: number;
  livingSpace: number;
  rooms: number;
  realEstateType: string;
  listingType: string;
  isPrivate: boolean;
  isProject: boolean;
  isNew: boolean;
  liveVideoTourAvailable: boolean;
  publishedLabel: string;
  address: Address;
  pictureUrls: string[];
  attributes: AttributeItem[];
  realtRealtorLogoUrl: string|null;
  
  //Клод как ты видишь я в бек енде вытащил его из адресной строки и сделал отдельным полем, чтобы было проще фильтровать по районам
  mappedDistrict: string;
}