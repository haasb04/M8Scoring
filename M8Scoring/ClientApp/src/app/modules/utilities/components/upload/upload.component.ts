import { Component, OnInit, Output, EventEmitter, Input, Inject } from '@angular/core';
import { HttpClient, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {
  public progress: number;
  public message: string;
  @Output() onUploadFinished:EventEmitter<any> = new EventEmitter();
  @Input() uploadUrl: string;

  constructor(private http:HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
  }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    var url = this.baseUrl + "/api/admin/uploadm8data";//this.uploadUrl;

    this.http.post(url, formData, { reportProgress: true, observe: 'events' })
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress) {
          this.progress = Math.round(100 * event.loaded / event.total);
        } else if (event.type === HttpEventType.Response) {
          this.message = "Upload success.";
          this.onUploadFinished.emit(event.body);
        }
      });
  }
}
