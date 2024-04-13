using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace AnanasMVCWebApp.Utilities {
    public class LimitCount : ValidationAttribute {
        private readonly int _min;
        private readonly int _max;
        public LimitCount(int min, int max) {
            _min = min;
            _max = max;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            var list = value as IList;
            if (list == null)
                return new ValidationResult(this.ErrorMessage);

            if (list.Count < _min || list.Count > _max)
                return new ValidationResult(this.ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
