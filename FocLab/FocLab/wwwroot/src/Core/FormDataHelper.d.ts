declare class FormDataHelper {
    static FillData(object: Object): void;
    /**
     * Собрать данные для свойств объекта с Html страницы
     * @param object   объект, свойства которого нужно заполнить
     * @param prefix   префикс стоящий перед свойствами объекта
     */
    static FillDataByPrefix(object: Object, prefix: string): void;
    static CollectData(object: Object): Object;
    /**
     *   Собрать данные с формы по префиксу
     * @param object  объект, свойства которого нужно собрать с формы
     * @param prefix  префикс для свойств объекта
     */
    static CollectDataByPrefix(object: Object, prefix: string): Object;
}
