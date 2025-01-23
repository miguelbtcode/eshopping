import { throwError } from 'rxjs';

export abstract class BaseService {

  protected handleError(error: any) {

    const applicationError = error.headers.get('Application-Error');

    if (applicationError) {
      return throwError(applicationError);
    }

    let modelStateErrors: string = '';

    for (let key in error.error) {
      if (error.error[key]) modelStateErrors += error.error[key].description + '\n';
    }
    //return;
    modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;
    return throwError(modelStateErrors || 'Server error');
  }
}
