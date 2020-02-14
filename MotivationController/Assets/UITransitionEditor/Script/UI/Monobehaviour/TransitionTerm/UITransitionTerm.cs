using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aojiru_UI
{
    //UIの遷移条件を管理する
    [System.Serializable]
    public class UITransitionTerm
    {
        //bool条件をすべて満たした状態で　トリガー条件を達成すると遷移可能
        //AbstractUITrrigerTerm _trrigerTerm;
        AbstractUITrrigerTerm _trrigerTerm;
        [SerializeField] TrrigerTermComponent _trrigerComponent;

        List<AbstractUIBoolTerm> _boolTerms = new List<AbstractUIBoolTerm>(); //bool条件　複数設定可能
        //public List<AbstractUIBoolTerm> _BoolTerms { get { return _boolTerms; } }
        [SerializeField] List<BoolTermComponent> _boolTermComponentList = new List<BoolTermComponent>();

        [HideInInspector, SerializeField] GameObject _termComponentObject;

        //遷移の条件を満たしている
        public bool IsMeetTerms()
        {
            if (_trrigerComponent.GetData() == null || _trrigerComponent.GetData().SatisfyTrriger._Trriger)
            {
                //トリガー条件を達成した状態で　bool条件をすべて満たすと遷移可能
                foreach (var term in _boolTermComponentList)
                {
                    //1つでも満たしていなければfalse
                    if (!term.GetData()._IsSatisfy) return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        #region Editor

        public void SyncComp2Data()
        {
            //var component = _termComponentObject.GetComponent<AbstractUITrrigerTerm>();
            //_trrigerTerm = component;
            _trrigerTerm = Data2CompConverter.SyncComp2Data<TrrigerTermComponent, AbstractUITrrigerTerm>(_termComponentObject);
            //var components = _termComponentObject.GetComponents<AbstractUIBoolTerm>();
            //_boolTerms = new List<AbstractUIBoolTerm>();
            //_boolTerms.AddRange(components);

            _boolTerms=Data2CompConverter.SyncComp2Datas<BoolTermComponent,AbstractUIBoolTerm>(_termComponentObject);
        }

        public void SyncData2Comp()
        {
            _trrigerComponent =
                Data2CompConverter.SyncData2Comp<TrrigerTermComponent, AbstractUITrrigerTerm>(_termComponentObject,_trrigerTerm);
            _boolTermComponentList=
                Data2CompConverter.SyncData2Comps<BoolTermComponent, AbstractUIBoolTerm>(_termComponentObject, _boolTerms);
        }
        #region trrigerTerm
        public void SetTrriger(AbstractUITrrigerTerm term)
        {
            _trrigerTerm = term;
        }

        public AbstractUITrrigerTerm GetTrriger()
        {
            return _trrigerTerm;
        }

        //public AbstractUITrrigerTerm AddTrrigerTerm(TrrigerType type)
        //{
        //    //var termType = AbstractUITrrigerTerm.GetTrrigerTermType(type);
        //    //var abst = _termComponentObject.AddComponent(termType) as AbstractUITrrigerTerm;
        //    //_trrigerTerm = abst;
        //    //return abst;
        //}
        //void RemoveTrrigerTerm(AbstractUITrrigerTerm term)
        //{
        //    //_boolTermsに所属していないとエラーを吐きそうだが対策をしていない
        //    MonoBehaviour.DestroyImmediate(term);
        //}

        //public AbstractUITrrigerTerm SetTrrigerTerm(AbstractUITrrigerTerm term, TrrigerType type)
        //{
        //    RemoveTrrigerTerm(term);
        //    var result = AddTrrigerTerm(type);
        //    return result;
        //}
        #endregion
        #region boolTerm
        public List<AbstractUIBoolTerm> GetBoolTerms()
        {
            return _boolTerms;
        }

        public AbstractUIBoolTerm SetBoolTerms(AbstractUIBoolTerm term, BoolTermType type)
        {
            _boolTerms.Remove(term);
            AbstractUIBoolTerm newTerm = null;
            switch (type)
            {
                case BoolTermType.AwakeTime:
                    newTerm = new AwakeTimeBoolTerm(); 
                    break;
                case BoolTermType.OnClickTime:
                    newTerm = new OnClickTimeBoolTerm();
                    break;
            }
            _boolTerms.Add(newTerm);
            return newTerm;
        }

        //public AbstractUIBoolTerm AddBoolTerm(BoolTermType type)
        //{
        //    var termType = AbstractUIBoolTerm.GetBoolTermType(type);
        //    var abst = _termComponentObject.AddComponent(typeof(BoolTermComponent))as BoolTermComponent;
        //    _boolTerms.Add(abst.GetData());
        //    return abst.GetData();
        //}

        //public void RemoveBoolTerm(int index)
        //{
        //    RemoveBoolTerm(_boolTerms[index]);
        //}

        //public void RemoveBoolTerm(AbstractUIBoolTerm term)
        //{
        //    //_boolTermsに所属していないとエラーを吐きそうだが対策をしていない
        //    MonoBehaviour.DestroyImmediate(term);
        //    _boolTerms.Remove(term);
        //}

        //public void SetBoolTerm(int index, BoolTermType type)
        //{
        //    SetBoolTerm(_boolTerms[index], type);
        //}

        //public AbstractUIBoolTerm SetBoolTerm(AbstractUIBoolTerm term, BoolTermType type)
        //{
        //    RemoveBoolTerm(term);
        //    var result = AddBoolTerm(type);
        //    return result;
        //}
        #endregion
        public void SetTermComponentObject(GameObject obj)
        {
            _termComponentObject = obj;
            SyncComp2Data();
        }
        #endregion
    }
}
