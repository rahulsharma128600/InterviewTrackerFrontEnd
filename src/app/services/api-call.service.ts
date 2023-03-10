import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class ApiCallService {

  constructor(private http : HttpClient) { }
  url = 'http://localhost:5170/api/Manager/GetMgr';

  trackerData()
  {
    return this.http.get(this.url);
  }

  trackerDates(dateFrom:Date, dateTo:Date)  {    
    //console.warn(dateFrom+" Last "+dateTo)    
    return this.http.get('http://localhost:5170/api/Panel/PanelByDate/'+dateFrom+'/'+dateTo)  
  }

  trackerPanelByMgr(mgrId:number)
  {
    return this.http.get('http://localhost:5170/api/Panel/GetPanelByMgr/'+mgrId)
  }

  trackerPanelByMultipleMgrs(str:string)
  {
    //console.warn(str);
    return this.http.get('http://localhost:5170/api/Panel/GetPanelByManagers/'+str)
  }

  trackerEmptyListMgrName(str:string){
    //console.warn('str in api',str);
    return this.http.get('http://localhost:5170/api/Manager/GetMgrById/'+str)
  }
}
