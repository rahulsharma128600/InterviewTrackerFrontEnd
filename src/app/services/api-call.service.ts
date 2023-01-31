import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class ApiCallService {

  constructor(private http : HttpClient) { }
  url = ' ';

  trackerData()
  {
    return this.http.get(this.url);
  }

  trackerDates(dateFrom:string, dateTo:string)  {    
    console.warn(dateFrom+" Last "+dateTo)    
    return this.http.get('http://localhost:5170/api/Panel/PanelByDate/'+dateFrom+'/'+dateTo)  
  }
}
