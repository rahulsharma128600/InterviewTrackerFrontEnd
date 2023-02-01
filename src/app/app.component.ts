import { Component } from '@angular/core';
import { ApiCallService } from './services/api-call.service'
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AssignmentFirst';
  fileName = 'ExcelSheet.xlsx';
  item: any;
  dateFrom: Date = new Date(0);
  dateTo: Date = new Date(0);
  availData: any;
  radioValue: any;
  panelList: any;
  errorMsg: any;

  constructor(private trackerItem: ApiCallService) {
    trackerItem.trackerData().subscribe((data) => {
      console.warn('data', data);
      this.item = data;
    })
  }

  validateDate(dateFrom: Date, dateTo: Date) {
    if ((dateFrom.toString() != "" && dateTo.toString() != "" && dateTo > dateFrom)) {
      return true
    }
    else if (dateFrom.toString() == "" || dateTo.toString() == "") {
      this.errorMsg = "Fields cannot be empty.Enter both the dates!!!";
      return false
    }
    else {
      this.errorMsg = "start date should be less than end date!!!"
      return false
    }
  }


  getAll() {
    this.trackerItem.trackerDates(this.dateFrom, this.dateTo).subscribe((data) => {
      console.warn(data)
      this.availData = data;
    })
  }

  getData(date1: any, date2: any) {
    if (this.validateDate(date1, date2)) {
      console.warn(date1, date2);
      this.dateFrom = date1;
      this.dateTo = date2;
      console.warn(this.dateFrom + " " + this.dateTo);
      this.getAll();
    }

  }

  getPanelByMgr(MgrId: any) {
    console.warn('mgrID : ', MgrId)
    this.trackerItem.trackerPanelByMgr(MgrId).subscribe((panel) => {
      console.warn('panel : ', panel)
      this.panelList = panel;
    })
  }

  radioChangeHandler(event: any) {
    this.radioValue = event.target.value;
  }


  // // Data to be Export in Excel 

  // userList = [
  //   {
  //     "id": 1,
  //     "name": "Leanne Graham",
  //     "username": "Bret",
  //     "email": "Sincere@april.biz"
  //   },

  //   {
  //     "id": 2,
  //     "name": "Ervin Howell",
  //     "username": "Antonette",
  //     "email": "Shanna@melissa.tv"
  //   },

  //   {
  //     "id": 3,
  //     "name": "Clementine Bauch",
  //     "username": "Samantha",
  //     "email": "Nathan@yesenia.net"
  //   },

  //   {
  //     "id": 4,
  //     "name": "Patricia Lebsack",
  //     "username": "Karianne",
  //     "email": "Julianne.OConner@kory.org"
  //   },

  //   {
  //     "id": 5,
  //     "name": "Chelsey Dietrich",
  //     "username": "Kamren",
  //     "email": "Lucio_Hettinger@annie.ca"
  //   }
  // ]

  exportexcel(): void {
    /* pass here the table id */
    let element = document.getElementById('excel-table');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);
  }
}